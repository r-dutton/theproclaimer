[web] GET /api/offices/{id:Guid}  (Workpapers.Next.API.Controllers.OfficeController.GetOffice)  [L73–L79] status=200
  └─ maps_to OfficeDto [L76]
  └─ uses_service UnitOfWork
    └─ method Table [L76]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ OfficeDto

