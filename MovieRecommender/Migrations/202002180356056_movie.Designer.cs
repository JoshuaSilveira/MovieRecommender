﻿// <auto-generated />
namespace MovieRecommender.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.4.0")]
    public sealed partial class movie : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(movie));
        
        string IMigrationMetadata.Id
        {
            get { return "202002180356056_movie"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}
