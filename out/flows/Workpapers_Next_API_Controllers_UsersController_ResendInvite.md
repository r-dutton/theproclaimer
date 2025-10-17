[web] POST /api/users/{id}/resendinvite  (Workpapers.Next.API.Controllers.UsersController.ResendInvite)  [L166–L176] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ calls UserRepository.WriteQuery [L170]
  └─ write User [L170]
  └─ uses_service UserRepository
    └─ method WriteQuery [L170]
      └─ implementation Workpapers.Next.Data.Repository.Firms.UserRepository.WriteQuery [L9-L41]
  └─ sends_request ResendIdentityInviteCommand -> ResendIdentityInviteCommandHandler [L173]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Identity.ResendIdentityInviteCommandHandler.Handle [L25–L50]
      └─ uses_service DataverseService
        └─ method Post [L39]
          └─ implementation Workpapers.Next.Dataverse.Service.DataverseService.Post [L17-L127]
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
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ User writes=1 reads=0
    └─ services 1
      └─ UserRepository
    └─ requests 1
      └─ ResendIdentityInviteCommand
    └─ handlers 1
      └─ ResendIdentityInviteCommandHandler
    └─ caches 1
      └─ IMemoryCache

