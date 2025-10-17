[web] PUT /api/templates/{id:Guid}/approve-changes/{approvedVersion}  (Workpapers.Next.API.Controllers.Templates.TemplatesController.ApproveChanges)  [L109–L121] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UnitOfWork
    └─ method Table [L113]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork

