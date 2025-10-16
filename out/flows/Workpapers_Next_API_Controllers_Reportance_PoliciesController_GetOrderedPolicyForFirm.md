[web] GET /api/reportsettings/policies/{id}/withsettings  (Workpapers.Next.API.Controllers.Reportance.PoliciesController.GetOrderedPolicyForFirm)  [L58–L76] status=200
  └─ maps_to OrderedPolicy [L75]
    └─ converts_to OrderedPolicy [L17]
  └─ uses_service IMapper
    └─ method Map [L75]
      └─ ... (no implementation details available)
  └─ uses_service UnitOfWork
    └─ method Table [L63]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetFirmId [L61]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]

