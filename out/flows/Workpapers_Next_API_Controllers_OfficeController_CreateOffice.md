[web] POST /api/offices  (Workpapers.Next.API.Controllers.OfficeController.CreateOffice)  [L102–L111] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ maps_to OfficeDto [L110]
  └─ uses_service IMapper
    └─ method Map [L110]
      └─ ... (no implementation details available)
  └─ uses_service UnitOfWork
    └─ method Add [L108]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetFirmId [L106]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]

