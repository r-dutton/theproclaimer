[web] GET /api/offices  (Workpapers.Next.API.Controllers.OfficeController.GetOffices)  [L58–L71] status=200
  └─ maps_to OfficeLightDto [L63]
  └─ uses_service UnitOfWork
    └─ method Table [L63]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetFirmId [L61]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]

