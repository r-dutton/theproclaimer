[web] GET /api/sections/{id:Guid}  (Workpapers.Next.API.Controllers.SectionsController.Get)  [L48–L54] status=200
  └─ maps_to SectionLightDto [L51]
  └─ uses_service UnitOfWork
    └─ method Table [L51]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ SectionLightDto

