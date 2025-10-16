[web] PUT /api/binder-column-template-sets  (Workpapers.Next.API.Controllers.Templates.BinderColumnTemplateSetsController.UpdateAll)  [L57–L63] status=200
  └─ sends_request CreateOrUpdateBinderColumnTemplateSets [L60]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Templates.BinderColumnTemplateSets.CreateOrUpdateBinderColumnTemplateSetsHandler.Handle [L22–L62]
      └─ uses_service IControlledRepository<BinderColumnTemplateSet>
        └─ method WriteQuery [L35]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L45]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

