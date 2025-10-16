[web] GET /api/binder-data/tax  (Workpapers.Next.API.Controllers.Workpapers.BinderDataController.ReadTaxDataFromDocument)  [L27–L34] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetTaxDataFromBinderQuery [L31]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.GetTaxDataFromBinderQueryHandler.Handle [L40–L360]
      └─ uses_service DataverseService
        └─ method GetStandardQueryParams [L355]
          └─ implementation Workpapers.Next.Dataverse.Service.DataverseService.GetStandardQueryParams [L17-L127]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L61]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L63]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

