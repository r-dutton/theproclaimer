[web] DELETE /api/templates/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.TemplatesController.Delete)  [L123–L136] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UnitOfWork
    └─ method Table [L127]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork

