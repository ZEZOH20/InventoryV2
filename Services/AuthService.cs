using AutoMapper;
using FluentValidation;
using InventoryV2.Dtos.AuthDtos.Requests;
using InventoryV2.Dtos.AuthDtos.Responses;
using InventoryV2.Interfaces.IServices;
using InventoryV2.Models;
using InventoryV2.Shares;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Net;


namespace InventoryV2.Services
{
    public class AuthService : IAuthService
    {
        /*
          UserManager<User> -> create, find, update users.
          RoleManager<IdentityRole> -> create, find, update roles.
          SignInManager<User> -> login , logout.
       */
        public UserManager<ApplicationUser> _manager;
        ITokenService _tokenService;
        ISendEmailService _sendEmailService;
        IOtpService _otpService;
        readonly IMapper _mapper;
      
        
        public AuthService(
            UserManager<ApplicationUser> manager,
            ITokenService tokenService,
            IOtpService otpService,
            ISendEmailService sendEmailService,
            IMapper mapper
            ) {
            _manager = manager;
            _tokenService = tokenService;
            _mapper = mapper;
            _sendEmailService = sendEmailService;
            _otpService = otpService;
        }
        public async Task<Response<AuthDto>> LoginAsync(LoginDto dto)
        {
            var user = await _manager.FindByEmailAsync(dto.Email);

            if (user is null || !await _manager.CheckPasswordAsync(user, dto.Password))
                return Response<AuthDto>.Failure("Invalid email or password", HttpStatusCode.Unauthorized);

            var userRoles = await _manager.GetRolesAsync(user);
            var token = _tokenService.GenerateToken(user , userRoles[0]);

            AuthDto response = new AuthDto
            {
                Id = user.Id,
                Email = dto.Email,
                UserName = user.UserName,
                Role = userRoles[0],
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresOn = token.ValidTo
            };

            return Response<AuthDto>.Success(response ,"Successfully");
        }

        public async Task<Response<AuthDto>> RegisterAsync(RegisterDto dto , CancellationToken cancellationToken)
        {
            //check if email id not exists
            if (await _manager.FindByEmailAsync(dto.Email) is not null)
                return Response<AuthDto>.Failure("Email is already registered", HttpStatusCode.Unauthorized);

            //check Otp is Correct
            bool IsVerified = await IsVerifiedEmail(dto.UserKey, dto.Otp, cancellationToken);
            if (!IsVerified)
                return Response<AuthDto>.Failure("Your email is not verified. Please verify it again");

            //register user email
            ApplicationUser user = _mapper.Map<ApplicationUser>(dto);
            var result = await _manager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return Response<AuthDto>.Failure("Registeration Fail" , HttpStatusCode.InternalServerError);

            // validation : Roles.All.Contains(dto.Role)
            //Add role to user
            await _manager.AddToRoleAsync(user, dto.Role);

            //generate stateless token
            var token = _tokenService.GenerateToken(user, dto.Role);

            AuthDto response = new AuthDto
            {
                Id = user.Id, 
                Email = dto.Email,
                UserName = dto.UserName,
                Role = dto.Role,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresOn = token.ValidTo
            };
            return Response<AuthDto>.Success(response, "Successfully");

            // send email otp verification

           
        }

        public async Task<Response> ResetPasswordAsync(ResetPasswordDto dto,CancellationToken cancellationToken)
        {
            var user = await _manager.FindByEmailAsync(dto.Email);
            if (user is null)
                return Response.Failure("Email doesn't exists");

            //check Otp is Correct
            bool IsVerified = await IsVerifiedEmail(dto.UserKey, dto.Otp, cancellationToken);
            if (!IsVerified)
                return Response.Failure("Your email is not verified. Please verify it again");

            //reset password
            var passwordResetToken = await _manager.GeneratePasswordResetTokenAsync(user);
            var result = await _manager.ResetPasswordAsync(user, passwordResetToken , dto.NewPassword);

            return Response.Success("Password reset Succesfully");

        }
        public async Task<Response<SendVerificationEmailRsDto>> SendVerificationEmailAsync(SendVerificationEmailRqDto dto, CancellationToken cancellationToken)
        {
            //check if email id not exists
                //if (await _manager.FindByEmailAsync(dto.Email) is not null)
                //    return Response<SendVerificationEmailRsDto>.Failure("Email is already registered");

            var otpGenerResult =  await _otpService.GenerateAndStoreOtpAsync(dto.Email, cancellationToken);
            var isSended = await _sendEmailService.SendVerificationEmail(dto.Email, otpGenerResult.Otp);

            if(!isSended) 
                Response<SendVerificationEmailRsDto>.Failure("Email is not valid or Network error try again");


            SendVerificationEmailRsDto response = new SendVerificationEmailRsDto
            {
                Otp = otpGenerResult.Otp,
                UserKey = otpGenerResult.UserKey,
                AvailableUntil = otpGenerResult.AvailableUntil
            };

            return Response<SendVerificationEmailRsDto>.Success(response,"Email Successfully Sended", HttpStatusCode.OK);
        }


        public async Task<bool> IsVerifiedEmail(string userKey, string otp , CancellationToken cancellationToken) 
              => await _otpService.ValidateOtpAsync(userKey, otp , cancellationToken);
    }
}
