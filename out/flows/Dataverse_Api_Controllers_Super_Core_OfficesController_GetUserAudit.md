[web] GET /api/super/offices/{officeId:Guid}/audit  (Dataverse.Api.Controllers.Super.Core.OfficesController.GetUserAudit)  [L102–L127] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls OfficeRepository.ReadQuery [L105]
  └─ query Office [L105]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L105]
      └─ ... (no implementation details available)

