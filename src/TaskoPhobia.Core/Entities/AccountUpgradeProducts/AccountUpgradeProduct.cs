﻿using TaskoPhobia.Core.Entities.AccountUpgradeProducts.ValueObjects;
using TaskoPhobia.Core.Entities.Products;
using TaskoPhobia.Core.Entities.Products.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.Money;

namespace TaskoPhobia.Core.Entities.AccountUpgradeProducts;

public class AccountUpgradeProduct : Product
{
    private AccountUpgradeProduct(ProductId id, ProductName name, Money price, ProductDescription description,
        AccountUpgradeTypeValue upgradeTypeValue) : base(
        id, name, price, description)
    {
        UpgradeTypeValue = upgradeTypeValue;
    }

    public AccountUpgradeProduct()
    {
    }

    public AccountUpgradeTypeValue UpgradeTypeValue { get; }

    public static AccountUpgradeProduct New(Guid id, ProductName name, Money price, ProductDescription description,
        AccountUpgradeTypeValue upgradeTypeValue)
    {
        return new AccountUpgradeProduct(id, name, price, description, upgradeTypeValue);
    }
}