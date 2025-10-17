[web] POST /api/workpaper-records/{id:guid}/create-or-update-financial-report  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.CreateOrUpdateFinancialReport)  [L450–L475] status=201 [auth=AuthorizationPolicies.User]
  └─ calls WorkpaperRecordRepository.WriteQuery [L453]
  └─ write WorkpaperRecord [L453]
    └─ reads_from WorkpaperRecords
  └─ sends_request CreateOrUpdateFinancialReportCommand -> CreateOrUpdateFinancialReportCommandHandler [L471]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.WorkpaperRecords.CreateOrUpdateFinancialReportCommandHandler.Handle [L42–L215]
      └─ uses_service DataverseService
        └─ method GetStandardQueryParams [L156]
          └─ implementation Workpapers.Next.Dataverse.Service.DataverseService.GetStandardQueryParams [L17-L127]
            └─ uses_service TenantIdentificationService
              └─ method GetStandardQueryParams [L70]
                └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetStandardQueryParams [L15-L131]
                  └─ uses_service RequestProcessor
                    └─ method GetCatalogByFirmId [L104]
                      └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                      └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                      └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                      └─ +1 additional_requests elided
                  └─ uses_service FirmLightDto
                    └─ method AssignActiveTenant [L77]
                      └─ implementation Workpapers.Next.DTOs.FirmLightDto.AssignActiveTenant [L8-L17]
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L116]
      └─ uses_service UserService
        └─ method GetUser [L118]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
            └─ uses_service RequestProcessor
              └─ method GetUserByDataverseId [L287]
                └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.GetUserByDataverseId
                └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetUserByDataverseId
                └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetUserByDataverseId
                └─ +1 additional_requests elided
            └─ uses_service bool?
              └─ method IsSuperUser [L174]
                └─ ... (no implementation details available)
            └─ uses_service Firm>
              └─ method GetFirmId [L139]
                └─ ... (no implementation details available)
            └─ uses_service User>
              └─ method GetUserIdFromToken [L106]
                └─ ... (no implementation details available)
            └─ uses_service User
              └─ method GetUserId [L67]
                └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
            └─ uses_service Guid?
              └─ method GetUserId [L64]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method ReadQuery [L106]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L94]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service ICirrusQueryService (AddScoped)
        └─ method GetReportData [L82]
          └─ implementation Workpapers.Next.CirrusServices.CirrusQueryService.GetReportData [L18-L250]
            └─ uses_service CirrusHttp
              └─ method GetAccounts [L33]
                └─ ... (no implementation details available)
            └─ uses_service CirrusConfig
              └─ method GetAccounts [L31]
                └─ ... (no implementation details available)
  └─ sends_request CreateOrUpdateFinancialReportCommand -> CreateOrUpdateFinancialReportCommandHandler [L466]
  └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L457]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.CanIAccessBinderQueryHandler.Handle [L60–L126]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L117]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method ReadQuery [L101]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L92]
          └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentTenant [L5-L22]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetCurrentTenant [L15-L131]
                  └─ uses_service RequestProcessor
                    └─ method GetCatalogByFirmId [L104]
                      └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                      └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                      └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                      └─ +1 additional_requests elided
                  └─ uses_service FirmLightDto
                    └─ method AssignActiveTenant [L77]
                      └─ implementation Workpapers.Next.DTOs.FirmLightDto.AssignActiveTenant (see previous expansion)
      └─ uses_service UserService
        └─ method GetUserId [L91]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
            └─ uses_service User
              └─ method GetUserId [L67]
                └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId (see previous expansion)
            └─ uses_service Guid?
              └─ method GetUserId [L64]
                └─ ... (no implementation details available)
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L89]
          └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
            └─ uses_service RequestInfo
              └─ method IsValidServiceAccountRequest [L71]
                └─ implementation Cirrus.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
                └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
                  └─ uses_service RequestInfo
                    └─ method IsValidServiceAccountRequest [L82]
                      └─ ... (service recursion detected)
                  └─ logs ILogger<IRequestInfoService> [Warning] [L89]
                └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest (see previous expansion)
            └─ logs ILogger<IRequestInfoService> [Warning] [L81]
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L121]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L109]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L92]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ WorkpaperRecord writes=1 reads=0
    └─ requests 2
      └─ CanIAccessBinderQuery
      └─ CreateOrUpdateFinancialReportCommand
    └─ handlers 2
      └─ CanIAccessBinderQueryHandler
      └─ CreateOrUpdateFinancialReportCommandHandler
    └─ caches 1
      └─ IMemoryCache

