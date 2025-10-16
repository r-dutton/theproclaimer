[web] POST /api/binders/  (Workpapers.Next.API.Controllers.Workpapers.BindersController.Create)  [L189–L242] status=201 [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.Add [L221]
  └─ insert Binder [L221]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method Add [L221]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUser [L229]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
  └─ sends_request CreateBinderDocumentCommand [L229]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Binders.CreateBinderDocumentCommandHandler.Handle [L32–L66]
      └─ uses_service DataverseService
        └─ method GetStandardQueryParams [L60]
          └─ implementation Workpapers.Next.Dataverse.Service.DataverseService.GetStandardQueryParams [L17-L127]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L51]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
  └─ sends_request CanBinderBeCreatedQuery [L215]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.CanBinderBeCreatedQueryHandler.Handle [L36–L151]
      └─ maps_to BinderTemplateDto [L77]
        └─ automapper.registration WorkpapersMappingProfile (BinderTemplate->BinderTemplateDto) [L726]
      └─ uses_service IControlledRepository<BinderTemplate>
        └─ method ReadQuery [L77]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate>
        └─ method ReadQuery [L117]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L63]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

