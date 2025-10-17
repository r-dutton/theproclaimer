[web] GET /api/useroffices/{id}  (Workpapers.Next.API.Controllers.UserOfficesController.Get)  [L48–L55] status=200
  └─ maps_to UserOfficeDto [L51]
  └─ uses_service UnitOfWork
    └─ method Table [L51]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ UserOfficeDto

