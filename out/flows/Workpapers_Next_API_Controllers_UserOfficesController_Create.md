[web] POST /api/useroffices/  (Workpapers.Next.API.Controllers.UserOfficesController.Create)  [L80–L90] status=201 [auth=admin]
  └─ uses_service UnitOfWork
    └─ method Add [L87]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetFirmId [L86]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]

