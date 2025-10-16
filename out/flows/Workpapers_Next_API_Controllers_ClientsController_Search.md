[web] GET /api/clients/  (Workpapers.Next.API.Controllers.ClientsController.Search)  [L45–L66] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetClientsQuery -> GetClientsQueryHandler [L61]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Clients.GetClientsQueryHandler.Handle [L70–L165]
      └─ calls ClientRepository.ReadQuery [L90]
      └─ uses_client ClientRepository [L90]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettings [L88]
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
        └─ method GetUserId [L87]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
            └─ uses_service User
              └─ method GetUserId [L67]
                └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
            └─ uses_service Guid?
              └─ method GetUserId [L64]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ impact_summary
    └─ clients 1
      └─ ClientRepository
    └─ requests 1
      └─ GetClientsQuery
    └─ handlers 1
      └─ GetClientsQueryHandler
    └─ caches 1
      └─ IMemoryCache

