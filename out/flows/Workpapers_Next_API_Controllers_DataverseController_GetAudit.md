[web] GET /api/dataverse/{entityRoute}/audit-entity/{dataverseId}  (Workpapers.Next.API.Controllers.DataverseController.GetAudit)  [L384–L394] [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ sends_request DataverseEntityAuditQuery [L391]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.DataverseEntityAuditQueryHandler.Handle [L24–L57]
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L50]
      └─ uses_service IControlledRepository<Entity>
        └─ method ReadQuery [L52]
      └─ uses_service IControlledRepository<Office>
        └─ method ReadQuery [L46]
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L48]
  └─ returns DataverseAuditDto [L391]

