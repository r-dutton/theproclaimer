[web] GET /api/binder-column-template-sets  (Workpapers.Next.API.Controllers.Templates.BinderColumnTemplateSetsController.GetAll)  [L27–L33] status=200
  └─ sends_request GetBinderColumnTemplateSets [L32]
  └─ sends_request GetBinderColumnTemplateSetsLight -> GetBinderColumnTemplateSetsHandler [L31]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Templates.GetBinderColumnTemplateSetsHandler.Handle [L17–L46]
      └─ uses_service IControlledRepository<BinderColumnTemplateSet> (Scoped (inferred))
        └─ method ReadQuery [L40]
          └─ implementation Workpapers.Next.Data.Repository.Templates.BinderColumnTemplateSetRepository.ReadQuery
  └─ impact_summary
    └─ requests 2
      └─ GetBinderColumnTemplateSets
      └─ GetBinderColumnTemplateSetsLight
    └─ handlers 1
      └─ GetBinderColumnTemplateSetsHandler

