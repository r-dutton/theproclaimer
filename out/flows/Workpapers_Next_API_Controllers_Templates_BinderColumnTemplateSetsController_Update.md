[web] PUT /api/binder-column-template-sets/{id:guid}  (Workpapers.Next.API.Controllers.Templates.BinderColumnTemplateSetsController.Update)  [L65–L71] status=200
  └─ sends_request UpdateBinderColumnTemplateSet -> UpdateBinderColumnTemplateSetHandler [L68]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Templates.BinderColumnTemplateSets.UpdateBinderColumnTemplateSetHandler.Handle [L32–L51]
      └─ uses_service IControlledRepository<BinderColumnTemplateSet> (Scoped (inferred))
        └─ method ReadQuery [L43]
          └─ implementation Workpapers.Next.Data.Repository.Templates.BinderColumnTemplateSetRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ UpdateBinderColumnTemplateSet
    └─ handlers 1
      └─ UpdateBinderColumnTemplateSetHandler

