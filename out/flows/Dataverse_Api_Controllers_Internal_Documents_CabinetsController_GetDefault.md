[web] GET /api/internal/cabinets/default  (Dataverse.Api.Controllers.Internal.Documents.CabinetsController.GetDefault)  [L32–L42] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to CabinetDto [L35]
    └─ automapper.registration DataverseMappingProfile (Cabinet->CabinetDto) [L398]
  └─ calls CabinetRepository.ReadQuery [L35]
  └─ query Cabinet [L35]
    └─ reads_from Cabinets
  └─ uses_service IControlledRepository<Cabinet>
    └─ method ReadQuery [L35]
      └─ ... (no implementation details available)

