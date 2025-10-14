[web] GET /api/licensing  (Workpapers.Next.API.Controllers.LicensingController.GetLicensePackage)  [L49–L95] [auth=AuthorizationPolicies.User]
  └─ maps_to FirmWithSubscriptionsDto (var Firm) [L86]
    └─ converts_to FirmWithStatsDto [L170]
  └─ maps_to UserDto (var User) [L85]
  └─ uses_service IMapper
    └─ method Map [L85]
  └─ uses_service UnitOfWork
    └─ method Table [L52]
  └─ uses_service UserService
    └─ method GetUserId [L53]

