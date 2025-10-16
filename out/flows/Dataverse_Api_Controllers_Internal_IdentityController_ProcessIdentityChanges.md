[web] PUT /api/internal/identity  (Dataverse.Api.Controllers.Internal.IdentityController.ProcessIdentityChanges)  [L46–L52] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ maps_to CentralUserDto [L49]
    └─ automapper.registration IdentityMappingProfile (IdentityAccount->CentralUserDto) [L44]
  └─ uses_service IMapper
    └─ method Map [L49]
      └─ ... (no implementation details available)
  └─ sends_request UpdateCentralUser [L49]
    └─ handled_by Cirrus.Central.Commands.UpdateCentralUserHandler.Handle [L37–L93]
      └─ calls CentralRepository.Commit [L77]
      └─ calls CentralRepository.WriteTable [L66]
      └─ calls CentralRepository.ReadTable [L53]
      └─ uses_service DataverseService
        └─ method GetStandardInviteInfo [L86]
          └─ implementation Cirrus.Central.Features.MachineToMachine.DataverseService.GetStandardInviteInfo [L14-L66]

