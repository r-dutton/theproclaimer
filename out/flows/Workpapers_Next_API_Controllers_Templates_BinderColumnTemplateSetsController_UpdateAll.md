[web] PUT /api/binder-column-template-sets  (Workpapers.Next.API.Controllers.Templates.BinderColumnTemplateSetsController.UpdateAll)  [L57–L63] status=200
  └─ sends_request CreateOrUpdateBinderColumnTemplateSets -> CreateOrUpdateBinderColumnTemplateSetsHandler [L60]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Templates.BinderColumnTemplateSets.CreateOrUpdateBinderColumnTemplateSetsHandler.Handle [L22–L62]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L45]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<BinderColumnTemplateSet> (Scoped (inferred))
        └─ method WriteQuery [L35]
          └─ implementation Workpapers.Next.Data.Repository.Templates.BinderColumnTemplateSetRepository.WriteQuery
  └─ impact_summary
    └─ requests 1
      └─ CreateOrUpdateBinderColumnTemplateSets
    └─ handlers 1
      └─ CreateOrUpdateBinderColumnTemplateSetsHandler

