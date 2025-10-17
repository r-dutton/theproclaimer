[web] DELETE /api/reportsettings/reporttemplates/{id:Guid}  (Workpapers.Next.API.Controllers.Reportance.ReportTemplatesController.Delete)  [L111–L127] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UnitOfWork
    └─ method Table [L115]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork

