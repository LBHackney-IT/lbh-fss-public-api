using AutoFixture;
using LBHFSSPublicAPI.V1.Domain;
using LBHFSSPublicAPI.V1.Infrastructure;

namespace LBHFSSPublicAPI.Tests.V1.Helper
{
    public static class DatabaseEntityHelper
    {
        public static Organization CreateOrganization()
        {
            var organizationEntity = new Fixture().Create<OrganizationEntity>();
            return CreateOrganizationFrom(organizationEntity);
        }

        public static Role CreateRole()
        {
            var roleEntity = new Fixture().Create<RoleEntity>();
            return CreateRoleFrom(roleEntity);
        }

        public static Service CreateService()
        {
            var serviceEntity = new Fixture().Create<ServiceEntity>();
            return CreateServiceFrom(serviceEntity);
        }

        public static ServiceLocation CreateServiceLocation()
        {
            var serviceLocationEntity = new Fixture().Create<ServiceLocationEntity>();
            return CreateServiceLocationFrom(serviceLocationEntity);
        }

        public static ServiceRevision CreateServiceRevision()
        {
            var serviceRevisionEntity = new Fixture().Create<ServiceRevisionEntity>();
            return CreateServiceRevisionFrom(serviceRevisionEntity);
        }

        public static Session CreateSession()
        {
            var sessionEntity = new Fixture().Create<SessionEntity>();
            return CreateSessionFrom(sessionEntity);
        }

        public static SynonymGroup CreateSynonymGroup()
        {
            var synonymGroupEntity = new Fixture().Create<SynonymGroupEntity>();
            return CreateSynonymGroupFrom(synonymGroupEntity);
        }

        public static SynonymWord CreateSynonymWord()
        {
            var synonymWordEntity = new Fixture().Create<SynonymWordEntity>();
            return CreateSynonymWordFrom(synonymWordEntity);
        }

        public static Taxonomy CreateTaxonomy()
        {
            var taxonomyEntity = new Fixture().Create<TaxonomyEntity>();
            return CreateTaxonomyFrom(taxonomyEntity);
        }

        public static User CreateUser()
        {
            var userEntity = new Fixture().Create<UserEntity>();
            return CreateUserFrom(userEntity);
        }

        public static UserOrganization CreateUserOrganization()
        {
            var userOrganizationEntity = new Fixture().Create<UserOrganizationEntity>();
            return CreateUserOrganizationFrom(userOrganizationEntity);
        }

        public static UserRole CreateUserRole()
        {
            var userRoleEntity = new Fixture().Create<UserRoleEntity>();
            return CreateUserRoleFrom(userRoleEntity);
        }

        /****************** PRIVATE METHODS ***********************/

        private static Organization CreateOrganizationFrom(OrganizationEntity organizationEntity)
        {
            // TODO: Reuse domain factory classes when created.
            return new Organization
            {
                Id = organizationEntity.Id,
                Name = organizationEntity.Name,
                CreatedAt = organizationEntity.CreatedAt
            };
        }

        private static UserRole CreateUserRoleFrom(UserRoleEntity userRoleEntity)
        {
            // TODO: Reuse domain factory classes when created.
            return new UserRole
            {
                Id = userRoleEntity.Id,
                RoleId = userRoleEntity.RoleId,
                CreatedAt = userRoleEntity.CreatedAt
            };
        }

        private static UserOrganization CreateUserOrganizationFrom(UserOrganizationEntity userOrganizationEntity)
        {
            // TODO: Reuse domain factory classes when created.
            return new UserOrganization
            {
                Id = userOrganizationEntity.Id,
                OrganizationId = userOrganizationEntity.OrganizationId,
                CreatedAt = userOrganizationEntity.CreatedAt
            };
        }

        private static Role CreateRoleFrom(RoleEntity roleEntity)
        {
            // TODO: Reuse domain factory classes when created.
            return new Role
            {
                Id = roleEntity.Id,
                Name = roleEntity.Name,
                CreatedAt = roleEntity.CreatedAt
            };
        }

        private static Service CreateServiceFrom(ServiceEntity serviceEntity)
        {
            // TODO: Reuse domain factory classes when created.
            return new Service
            {
                Id = serviceEntity.Id,
                RevisionId = serviceEntity.RevisionId,
                OrganizationId = serviceEntity.OrganizationId,
                CreatedAt = serviceEntity.CreatedAt
            };
        }

        private static ServiceLocation CreateServiceLocationFrom(ServiceLocationEntity serviceLocationEntity)
        {
            // TODO: Reuse domain factory classes when created.
            return new ServiceLocation
            {
                Id = serviceLocationEntity.Id,
                Address1 = serviceLocationEntity.Address1,
                City = serviceLocationEntity.City,
                Country = serviceLocationEntity.Country,
                Latitude = serviceLocationEntity.Latitude,
                Longitude = serviceLocationEntity.Longitude,
                PostalCode = serviceLocationEntity.PostalCode,
                RevisionId = serviceLocationEntity.RevisionId,
                StateProvince = serviceLocationEntity.StateProvince,
                Uprn = serviceLocationEntity.Uprn,
                CreatedAt = serviceLocationEntity.CreatedAt
            };
        }

        private static ServiceRevision CreateServiceRevisionFrom(ServiceRevisionEntity serviceRevisionEntity)
        {
            // TODO: Reuse domain factory classes when created.
            return new ServiceRevision
            {
                Id = serviceRevisionEntity.Id,
                AuthorId = serviceRevisionEntity.AuthorId,
                Description = serviceRevisionEntity.Description,
                Facebook = serviceRevisionEntity.Facebook,
                Instagram = serviceRevisionEntity.Instagram,
                Linkedin = serviceRevisionEntity.Linkedin,
                Name = serviceRevisionEntity.Name,
                ReviewedAt = serviceRevisionEntity.ReviewedAt,
                ReviewerMessage = serviceRevisionEntity.ReviewerMessage,
                ReviewerUid = serviceRevisionEntity.ReviewerUid,
                ServiceId = serviceRevisionEntity.ServiceId,
                Status = serviceRevisionEntity.Status,
                SubmittedAt = serviceRevisionEntity.SubmittedAt,
                Telephone = serviceRevisionEntity.Telephone,
                Twitter = serviceRevisionEntity.Twitter,
                Website = serviceRevisionEntity.Website,
                CreatedAt = serviceRevisionEntity.CreatedAt
            };
        }

        private static Session CreateSessionFrom(SessionEntity sessionEntity)
        {
            // TODO: Reuse domain factory classes when created.
            return new Session
            {
                Id = sessionEntity.Id,
                IpAddress = sessionEntity.IpAddress,
                LastAccessAt = sessionEntity.LastAccessAt,
                Payload = sessionEntity.Payload,
                UserAgent = sessionEntity.UserAgent,
                UserId = sessionEntity.UserId,
                CreatedAt = sessionEntity.CreatedAt
            };
        }

        private static SynonymGroup CreateSynonymGroupFrom(SynonymGroupEntity synonymGroupEntity)
        {
            // TODO: Reuse domain factory classes when created.
            return new SynonymGroup
            {
                Id = synonymGroupEntity.Id,
                Name = synonymGroupEntity.Name,
                CreatedAt = synonymGroupEntity.CreatedAt
            };
        }

        private static SynonymWord CreateSynonymWordFrom(SynonymWordEntity synonymWordEntity)
        {
            // TODO: Reuse domain factory classes when created.
            return new SynonymWord
            {
                Id = synonymWordEntity.Id,
                GroupId = synonymWordEntity.GroupId,
                Word = synonymWordEntity.Word,
                CreatedAt = synonymWordEntity.CreatedAt
            };
        }

        private static Taxonomy CreateTaxonomyFrom(TaxonomyEntity taxonomyEntity)
        {
            // TODO: Reuse domain factory classes when created.
            return new Taxonomy
            {
                Id = taxonomyEntity.Id,
                Name = taxonomyEntity.Name,
                ParentId = taxonomyEntity.ParentId,
                Vocabulary = taxonomyEntity.Vocabulary,
                CreatedAt = taxonomyEntity.CreatedAt
            };
        }

        private static User CreateUserFrom(UserEntity userEntity)
        {
            // TODO: Reuse domain factory classes when created.
            return new User
            {
                Id = userEntity.Id,
                SubId = userEntity.SubId,
                Email = userEntity.Email,
                Name = userEntity.Name,
                CreatedAt = userEntity.CreatedAt,
                Status = userEntity.Status
            };
        }
    }
}
