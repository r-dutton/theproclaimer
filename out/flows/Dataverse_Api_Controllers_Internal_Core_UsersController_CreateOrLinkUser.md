[web] POST /api/internal/users/create-or-link  (Dataverse.Api.Controllers.Internal.Core.UsersController.CreateOrLinkUser)  [L144–L150] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to UserWithIdentityInfoDto [L149]
  └─ uses_service IMapper
    └─ method Map [L149]
  └─ sends_request CreateOrUpdateUserCommand [L147]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Users.CreateOrLinkWithDataverseCommandHandler.Handle [L57–L149]
      └─ uses_service EventPublisher (InstancePerLifetimeScope)
        └─ method PublishAllEventsForEntity [L145]
      └─ uses_service IControlledRepository<FirmSettings>
        └─ method ReadQuery [L111]
      └─ uses_service IControlledRepository<Office>
        └─ method ReadQuery [L86]
      └─ uses_service IControlledRepository<Team>
        └─ method ReadQuery [L87]
      └─ uses_service IControlledRepository<User>
        └─ method WriteQuery [L89]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L141]
      └─ uses_service UserService
        └─ method IsInRole [L103]

