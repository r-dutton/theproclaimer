[web] PUT /api/internal/identity  (Dataverse.Api.Controllers.Internal.IdentityController.ProcessIdentityChanges)  [L46–L52] [auth=Authentication.MachineToMachinePolicy]
  └─ maps_to CentralUserDto [L49]
    └─ automapper.registration IdentityMappingProfile (IdentityAccount->CentralUserDto) [L44]
  └─ uses_service IMapper
    └─ method Map [L49]
  └─ sends_request UpdateCentralUser [L49]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.Central.Commands.UpdateCentralUserHandler.Handle [L37–L93]
      └─ calls CentralRepository.Commit [L77]
      └─ calls CentralRepository.WriteTable [L66]
      └─ calls CentralRepository.ReadTable [L53]
      └─ uses_service DataverseService
        └─ method GetStandardInviteInfo [L86]

