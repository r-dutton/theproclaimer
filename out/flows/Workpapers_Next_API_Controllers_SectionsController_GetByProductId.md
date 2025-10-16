[web] GET /api/sections/byproductnumber/{id:int}  (Workpapers.Next.API.Controllers.SectionsController.GetByProductId)  [L65–L81] status=200
  └─ maps_to SectionDto [L68]
  └─ uses_service UnitOfWork
    └─ method Table [L68]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ SectionDto

