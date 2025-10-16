[web] GET /api/internal/workpapers/binders/{binderId:Guid}/tax-info  (Workpapers.Next.API.Controllers.Internal.Workpapers.BindersController.ReadTaxDataFromDocument)  [L41–L45] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ sends_request GetTaxDataFromBinderQuery -> GetTaxDataFromBinderQueryHandler [L44]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.GetTaxDataFromBinderQueryHandler.Handle [L40–L360]
      └─ uses_service DataverseService
        └─ method GetStandardQueryParams [L355]
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
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L63]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method ReadQuery [L61]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetTaxDataFromBinderQuery
    └─ handlers 1
      └─ GetTaxDataFromBinderQueryHandler
    └─ caches 1
      └─ IMemoryCache

