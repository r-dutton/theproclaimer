[web] GET /api/loan-matrices/for-client-group/{clientGroupId:guid}  (Workpapers.Next.API.Controllers.Workpapers.LoanMatricesController.GetForClientGroup)  [L47–L51] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetLoanMatrixByClientGroupQuery -> GetLoanMatrixByClientGroupQueryHandler [L50]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.LoanMatrices.GetLoanMatrixByClientGroupQueryHandler.Handle [L36–L94]
      └─ calls ClientRepository.ReadQuery [L82]
      └─ maps_to LoanMatrixDto [L63]
        └─ automapper.registration WorkpapersMappingProfile (LoanMatrix->LoanMatrixDto) [L997]
      └─ uses_client ClientRepository [L82]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettings [L84]
          └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.GetCurrentSettings [L23-L154]
            └─ uses_service TenantService
              └─ method GetCurrentSettings [L46]
                └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentSettings [L5-L22]
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
            └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L60]
      └─ uses_service UserService
        └─ method GetUser [L84]
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
      └─ uses_service IControlledRepository<LoanMatrix> (Scoped (inferred))
        └─ method ReadQuery [L63]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.LoanMatrixRepository.ReadQuery
  └─ impact_summary
    └─ clients 1
      └─ ClientRepository
    └─ requests 1
      └─ GetLoanMatrixByClientGroupQuery
    └─ handlers 1
      └─ GetLoanMatrixByClientGroupQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ LoanMatrixDto

