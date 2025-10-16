[web] PUT /api/binders/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BindersController.Update)  [L281–L311] status=200 [auth=AuthorizationPolicies.User]
  └─ calls SourceRepository.ReadQuery [L297]
  └─ calls BinderRepository.WriteQuery [L285]
  └─ query Source [L297]
  └─ write Binder [L285]
    └─ reads_from Binders
  └─ uses_service SourceRepository
    └─ method ReadQuery [L297]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceRepository.ReadQuery [L8-L40]
  └─ sends_request AreBinderColumnDatasetsValidQuery -> AreBinderColumnDatasetsValidQueryHandler [L304]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.AreBinderColumnDatasetsValidQueryHandler.Handle [L29–L76]
      └─ uses_service ICirrusQueryService (AddScoped)
        └─ method GetDatasetsForFile [L42]
          └─ implementation Workpapers.Next.CirrusServices.CirrusQueryService.GetDatasetsForFile [L18-L250]
            └─ uses_service CirrusHttp
              └─ method GetAccounts [L33]
                └─ ... (no implementation details available)
            └─ uses_service CirrusConfig
              └─ method GetAccounts [L31]
                └─ ... (no implementation details available)
  └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L291]
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
                      └─ implementation Workpapers.Next.DTOs.FirmLightDto.AssignActiveTenant [L8-L17]
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L116]
      └─ uses_service UserService
        └─ method GetUserId [L91]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
            └─ uses_service User
              └─ method GetUserId [L67]
                └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
            └─ uses_service Guid?
              └─ method GetUserId [L64]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
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
    └─ entities 2 (writes=1, reads=1)
      └─ Binder writes=1 reads=0
      └─ Source writes=0 reads=1
    └─ services 1
      └─ SourceRepository
    └─ requests 2
      └─ AreBinderColumnDatasetsValidQuery
      └─ CanIAccessBinderQuery
    └─ handlers 2
      └─ AreBinderColumnDatasetsValidQueryHandler
      └─ CanIAccessBinderQueryHandler
    └─ caches 1
      └─ IMemoryCache

