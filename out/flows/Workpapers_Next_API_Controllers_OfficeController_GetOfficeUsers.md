[web] GET /api/offices/{id:Guid}/users  (Workpapers.Next.API.Controllers.OfficeController.GetOfficeUsers)  [L81–L91] status=200
  └─ maps_to UserOfficeDto [L84]
  └─ uses_service UnitOfWork
    └─ method Table [L84]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ UserOfficeDto

