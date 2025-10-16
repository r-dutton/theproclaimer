[web] GET /api/licensing  (Workpapers.Next.API.Controllers.LicensingController.GetLicensePackage)  [L49–L95] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to FirmWithSubscriptionsDto (var Firm) [L86]
    └─ converts_to FirmWithStatsDto [L170]
  └─ maps_to UserDto (var User) [L85]
  └─ uses_service IMapper
    └─ method Map [L85]
      └─ ... (no implementation details available)
  └─ uses_service UnitOfWork
    └─ method Table [L52]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUserId [L53]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]

