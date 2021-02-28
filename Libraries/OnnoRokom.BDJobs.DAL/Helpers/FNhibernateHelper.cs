using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OnnoRokom.BDJobs.DAL.Helpers
{
    public class FNhibernateHelper
    {
        public static ISession OpenSession(string connectionString)
        {
            var assemblies = new List<Assembly>()
            {
                Assembly.Load("OnnoRokom.BDJobs.JobsLib")
            };

            var sessionFactory = Fluently.Configure()
                .Database(
                    FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2012
                        .ConnectionString(connectionString)
                        .ShowSql())

                .Mappings(m => assemblies.ForEach(a => m.FluentMappings.AddFromAssembly(a)))
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true))
                .BuildSessionFactory();

            return sessionFactory.OpenSession();
        }
    }
}
