[web] PUT /api/accounting/reports/labels/account/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Layout.ReportLabelController.UpdateAccountReportLabels)  [L81–L90] [auth=Authentication.UserPolicy]
  └─ calls AccountRepository.WriteQuery [L84]
  └─ writes_to Account [L84]
  └─ uses_service IControlledRepository<Account>
    └─ method WriteQuery [L84]
  └─ sends_request CanIAccessFileQuery [L85]
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

