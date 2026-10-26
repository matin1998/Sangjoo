using Microsoft.AspNetCore.Identity;
using Sangjoo.Application.DTOs.AdminSide.Sellers;
using Sangjoo.Application.Services.Interfaces;
using Sangjoo.Domain.Entities;
using Sangjoo.Domain.RepositoryInterfaces;
using Sangjoo.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Application.Services.Implementations;
public class SellerService : ISellerService
{
    private readonly ISellerRepository _sellerRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public SellerService(
        ISellerRepository sellerRepository,
        UserManager<ApplicationUser> userManager,
        IUnitOfWork unitOfWork)
    {
        _sellerRepository = sellerRepository;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> CreateSellerAsync(CreateSellerDTO model)
    {

        var existingUser = await _userManager.FindByEmailAsync(model.Email);

        if (existingUser != null)
            return false;

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var seller = new Seller
            {
                Name = model.SellerName,
                IsActive = true
            };

            await _sellerRepository.AddAsync(seller);
            await _unitOfWork.SaveChangesAsync();

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                FullName = model.FullName,
                IsActive = model.IsActive,
                Seller = seller
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return false;
            }

            var roleResult = await _userManager.AddToRoleAsync(user, "Seller");

            if (!roleResult.Succeeded)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return false;
            }

            await _unitOfWork.CommitTransactionAsync();

            return true;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
    public async Task<long?> GetSellerIdByUserIdAsync(long userId)
    {
        var seller = await _sellerRepository.GetByUserIdAsync(userId);

        return seller?.Id;
    }
}
