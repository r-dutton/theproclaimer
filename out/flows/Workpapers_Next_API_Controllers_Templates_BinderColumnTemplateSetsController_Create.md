[web] POST /api/binder-column-template-sets  (Workpapers.Next.API.Controllers.Templates.BinderColumnTemplateSetsController.Create)  [L47–L55]
  └─ sends_request CreateBinderColumnTemplateSet [L52]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Templates.BinderColumnTemplateSets.CreateBinderColumnTemplateSetHandler.Handle [L23–L58]
      └─ uses_service IControlledRepository<BinderColumnTemplateSet>
        └─ method Add [L37]

