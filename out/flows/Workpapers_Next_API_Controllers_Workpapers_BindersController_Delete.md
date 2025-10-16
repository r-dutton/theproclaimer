[web] DELETE /api/binders/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BindersController.Delete)  [L475–L511] status=204 [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.Remove [L502]
  └─ calls BinderRepository.WriteQuery [L481]
  └─ delete Binder [L502]
    └─ reads_from Binders
  └─ write Binder [L481]
    └─ reads_from Binders
  └─ uses_service DataverseService
    └─ method GetStandardQueryParams [L490]
      └─ implementation Workpapers.Next.Dataverse.Service.DataverseService.GetStandardQueryParams [L17-L127]
  └─ uses_service IControlledRepository<Binder>
    └─ method WriteQuery [L481]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUser [L491]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
  └─ sends_request CreateDataverseAuditHistoryCommand [L504]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Firm.CreateDataverseAuditHistoryQueryHandler.Handle [L37–L84]
      └─ uses_service DataverseService
        └─ method Post [L75]
          └─ implementation Cirrus.Central.Features.MachineToMachine.DataverseService.Post [L14-L66]
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L52]
          └─ implementation ITenantService.GetCurrentTenant [L14-L14]
          └─ ... (no implementation details available)
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUser [L53]
          └─ implementation IUserService.GetUser [L18-L18]
          └─ ... (no implementation details available)
  └─ sends_request CanIAccessBinderQuery [L483]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.CanIAccessBinderQueryHandler.Handle [L60–L126]
      └─ uses_service UserService
        └─ method GetUserId [L91]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L89]
          └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
          └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
          └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L92]
          └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentTenant [L5-L22]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetCurrentTenant [L15-L131]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L101]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L117]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L121]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L109]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L92]

