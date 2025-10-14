[web] GET /api/accounting/assets/reports/monthly  (Cirrus.Api.Controllers.Accounting.Assets.AssetReportController.GetMonthlyDepreciationReport)  [L123–L137] [auth=user]
  └─ sends_request CanIAccessFileQuery [L126]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessFileQueryHandler.Handle [L43–L101]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L66]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L90]
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L68]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L68]
      └─ uses_cache IDistributedCache [L79]
        └─ method SetRecordAsync [write] [L79]
      └─ uses_cache IDistributedCache [L71]
        └─ method DoesRecordExistAsync [access] [L71]
      └─ uses_cache IDistributedCache [L68]
        └─ method CreateAccessKey [write] [L68]
  └─ sends_request GetMonthlyDepreciationQuery [L128]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Assets.GetMonthlyDepreciationQueryHandler.Handle [L19–L183]
      └─ uses_service GetDepreciationDetailForMonthlyQuery
        └─ method Execute [L37]
      └─ uses_service IControlledRepository<DepreciationYear>
        └─ method ReadQuery [L38]

