[web] POST /api/binder-column-template-sets  (Workpapers.Next.API.Controllers.Templates.BinderColumnTemplateSetsController.Create)  [L47–L55] status=201
  └─ sends_request CreateBinderColumnTemplateSet -> CreateBinderColumnTemplateSetHandler [L52]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Templates.BinderColumnTemplateSets.CreateBinderColumnTemplateSetHandler.Handle [L23–L58]
      └─ uses_service IControlledRepository<BinderColumnTemplateSet> (Scoped (inferred))
        └─ method Add [L37]
          └─ implementation Workpapers.Next.Data.Repository.Templates.BinderColumnTemplateSetRepository.Add
  └─ impact_summary
    └─ requests 1
      └─ CreateBinderColumnTemplateSet
    └─ handlers 1
      └─ CreateBinderColumnTemplateSetHandler

