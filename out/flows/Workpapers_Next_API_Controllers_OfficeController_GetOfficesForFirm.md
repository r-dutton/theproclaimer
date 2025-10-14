[web] GET /api/offices/byfirm/{firmId:Guid}  (Workpapers.Next.API.Controllers.OfficeController.GetOfficesForFirm)  [L45–L56] [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to OfficeLightDto [L49]
  └─ uses_service UnitOfWork
    └─ method Table [L49]

