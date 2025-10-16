[web] DELETE /api/binder-column-template-sets/{id:guid}  (Workpapers.Next.API.Controllers.Templates.BinderColumnTemplateSetsController.Delete)  [L73–L81] status=204
  └─ sends_request DeleteBinderColumnTemplateSet -> DeleteBinderColumnTemplateSetHandler [L78]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Templates.BinderColumnTemplateSets.DeleteBinderColumnTemplateSetHandler.Handle [L26–L44]
      └─ uses_service IControlledRepository<BinderColumnTemplateSet> (Scoped (inferred))
        └─ method ReadQuery [L37]
          └─ implementation Workpapers.Next.Data.Repository.Templates.BinderColumnTemplateSetRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ DeleteBinderColumnTemplateSet
    └─ handlers 1
      └─ DeleteBinderColumnTemplateSetHandler

