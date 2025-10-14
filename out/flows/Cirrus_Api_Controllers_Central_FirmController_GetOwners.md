[web] GET /api/central/firms/{firmId}/owners  (Cirrus.Api.Controllers.Central.FirmController.GetOwners)  [L74–L83] [auth=Authentication.MachineToMachinePolicy]
  └─ maps_to FirmUserDto [L77]
  └─ calls CentralRepository.ReadTable [L77]
  └─ uses_service CentralRepository
    └─ method ReadTable [L77]

