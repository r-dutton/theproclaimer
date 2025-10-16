[web] POST /api/binder-column-template-sets  (Workpapers.Next.API.Controllers.Templates.BinderColumnTemplateSetsController.Create)  [L47–L55] status=201
  └─ sends_request CreateBinderColumnTemplateSet [L52]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Templates.BinderColumnTemplateSets.CreateBinderColumnTemplateSetHandler.Handle [L23–L58]
      └─ uses_service IControlledRepository<BinderColumnTemplateSet>
        └─ method Add [L37]
          └─ ... (no implementation details available)

