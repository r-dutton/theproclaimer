[web] POST /api/offices  (Workpapers.Next.API.Controllers.OfficeController.CreateOffice)  [L102–L111] [auth=AuthorizationPolicies.Administrator]
  └─ maps_to OfficeDto [L110]
  └─ uses_service IMapper
    └─ method Map [L110]
  └─ uses_service UnitOfWork
    └─ method Add [L108]
  └─ uses_service UserService
    └─ method GetFirmId [L106]

