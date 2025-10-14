[web] PUT /api/binder-column-template-sets  (Workpapers.Next.API.Controllers.Templates.BinderColumnTemplateSetsController.UpdateAll)  [L57–L63]
  └─ sends_request CreateOrUpdateBinderColumnTemplateSets [L60]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Templates.BinderColumnTemplateSets.CreateOrUpdateBinderColumnTemplateSetsHandler.Handle [L22–L62]
      └─ uses_service IControlledRepository<BinderColumnTemplateSet>
        └─ method WriteQuery [L35]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L45]

