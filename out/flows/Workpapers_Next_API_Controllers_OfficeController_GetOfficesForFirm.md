[web] GET /api/offices/byfirm/{firmId:Guid}  (Workpapers.Next.API.Controllers.OfficeController.GetOfficesForFirm)  [L45–L56] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to OfficeLightDto [L49]
  └─ uses_service UnitOfWork
    └─ method Table [L49]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ OfficeLightDto

