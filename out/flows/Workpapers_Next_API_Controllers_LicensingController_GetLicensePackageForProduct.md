[web] GET /api/licensing/{productExcelId:int}  (Workpapers.Next.API.Controllers.LicensingController.GetLicensePackageForProduct)  [L97–L130] [auth=AuthorizationPolicies.User]
  └─ maps_to FirmDto (var Firm) [L106]
    └─ converts_to FirmWithStatsDto [L162]
  └─ maps_to UserDto (var User) [L105]
  └─ uses_service IMapper
    └─ method Map [L105]
  └─ uses_service UnitOfWork
    └─ method Table [L100]
  └─ uses_service UserService
    └─ method GetUserId [L100]
  └─ sends_request GetProductQuery [L113]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Licensing.Products.GetProductQueryHandler.Handle [L57–L101]
      └─ uses_service UnitOfWork
        └─ method Table [L71]

