using promoit_backend_cs_api.Models;
using promoit_backend_cs_api.ModelsDTO;

namespace promoit_backend_cs.Services
{
    public class DTOService
    {
        public static BcrDTO BcrToDTO(Bcr bcr) => new BcrDTO
        {
            Id = bcr.Id,
            CompanyName = bcr.CompanyName,
            UserId = bcr.UserId,
            UpdateDate = bcr.UpdateDate,
            CreateUserId = bcr.CreateUserId,
            UpdateUserId = bcr.UpdateUserId,
            StatusId = bcr.StatusId
        };

        public static CampaignDTO CampaignToDTO(Campaign product) => new CampaignDTO
        {
            Id = product.Id,
            Name = product.Name,
            Link = product.Link,
            Hashtag = product.Hashtag,
            NprId = product.NprId,
            CreateDate = product.CreateDate,
            UpdateDate = product.UpdateDate,
            CreateUserId = product.CreateUserId,
            UpdateUserId = product.UpdateUserId,
            StatusId = product.StatusId
        };

        public static NonProfitRepresentativeDTO NonProfitRepresentativeToDTO(NonProfitRepresentative nonProfitRepresentative) => new NonProfitRepresentativeDTO
        {
            Id = nonProfitRepresentative.Id,
            Email = nonProfitRepresentative.Email,
            OrganizationName = nonProfitRepresentative.OrganizationName,
            OrganizationLink = nonProfitRepresentative.OrganizationLink,
            UserId = nonProfitRepresentative.UserId,
            CreateDate = nonProfitRepresentative.CreateDate,
            UpdateDate = nonProfitRepresentative.UpdateDate,
            CreateUserId = nonProfitRepresentative.CreateUserId,
            UpdateUserId = nonProfitRepresentative.UpdateUserId,
            StatusId = nonProfitRepresentative.StatusId
        };

        public static ProductDTO ProductToDTO(Product product) => new ProductDTO
        {
            Id = product.Id,
            Name = product.Name,
            Value = product.Value,
            BcrId = product.BcrId,
            CreateDate = product.CreateDate,
            UpdateDate = product.UpdateDate,
            CreateUserId = product.CreateUserId,
            UpdateUserId = product.UpdateUserId,
            StatusId = product.StatusId
        };

        public static ProductToCampaignDTO ProductToCampaignToDTO(ProductToCampaign productToCampaign) => new ProductToCampaignDTO
        {
            Id = productToCampaign.Id,
            CampaignId = productToCampaign.CampaignId,
            ProductId = productToCampaign.ProductId,
            InititalNumber = productToCampaign.InititalNumber,
            BoughtNumber = productToCampaign.BoughtNumber,
            CreateDate = productToCampaign.CreateDate,
            UpdateDate = productToCampaign.UpdateDate,
            CreateUserId = productToCampaign.CreateUserId,
            UpdateUserId = productToCampaign.UpdateUserId,
            StatusId = productToCampaign.StatusId
        };



        public static SaToCampaignDTO SaToCampaignDTO(SaToCampaign saToCampaign) => new SaToCampaignDTO
        {
            Id = saToCampaign.Id,
            SocialActivistId = saToCampaign.SocialActivistId,
            CampaignId = saToCampaign.CampaignId,
            Money = saToCampaign.Money,
            CreateDate = saToCampaign.CreateDate,
            UpdateDate = saToCampaign.UpdateDate,
            CreateUserId = saToCampaign.CreateUserId,
            UpdateUserId = saToCampaign.UpdateUserId,
            StatusId = saToCampaign.StatusId
        };
    }

}
