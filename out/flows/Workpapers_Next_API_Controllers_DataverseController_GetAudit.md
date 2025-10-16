[web] GET /api/dataverse/{entityRoute}/audit-entity/{dataverseId}  (Workpapers.Next.API.Controllers.DataverseController.GetAudit)  [L385–L395] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ sends_request DataverseEntityAuditQuery [L392]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.DataverseEntityAuditQueryHandler.Handle [L24–L57]
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L50]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Entity>
        └─ method ReadQuery [L52]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Office>
        └─ method ReadQuery [L46]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L48]
          └─ ... (no implementation details available)
  └─ returns DataverseAuditDto [L392]

