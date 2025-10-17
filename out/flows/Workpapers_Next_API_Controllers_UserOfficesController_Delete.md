[web] DELETE /api/useroffices/{id:int}  (Workpapers.Next.API.Controllers.UserOfficesController.Delete)  [L107–L115] status=200 [auth=admin]
  └─ uses_service UnitOfWork
    └─ method Table [L110]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork

