[web] GET /api/licensing/{productExcelId:int}  (Workpapers.Next.API.Controllers.LicensingController.GetLicensePackageForProduct)  [L97–L130] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to FirmDto (var Firm) [L106]
    └─ converts_to FirmWithStatsDto [L162]
  └─ maps_to UserDto (var User) [L105]
  └─ uses_service IMapper
    └─ method Map [L105]
      └─ ... (no implementation details available)
  └─ uses_service UnitOfWork
    └─ method Table [L100]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUserId [L100]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
  └─ sends_request GetProductQuery [L113]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Licensing.Products.GetProductQueryHandler.Handle [L57–L101]
      └─ uses_service UnitOfWork
        └─ method Table [L71]
          └─ ... (no implementation details available)

