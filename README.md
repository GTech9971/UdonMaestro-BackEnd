# UdonMaestro-BackEnd

## 目次

## 開発環境
- VisualStudio 2022
- .NET 6.0
- Microsoft.EntityFrameworkCore 6.0.9
- Microsoft.EntityFrameworkCore.Tools 6.0.9
- Npgsql.EntityFrameworkCore.PostgreSQL 6.0.7


## EntityFrameworkインストール
### NuGetからライブラリをインストール
Microsoft.EntityframeworkCoreで検索し、以下をインストールする
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Tools
- Npgsql.EntityFrameworkCore.PostgreSQL

### EntityFrameworkコマンドを有効化
ツール > NuGetパッケージマネジャー > パッケージマネジャーコンソールを開く
以下のコマンドを実行する
`dotnet tool install --global dotnet-ef`

以下のコマンドを実行して結果が返ってくれば良い
`dotnet-ef`

## PostgreSQLに接続する準備
1. ソリューションの`appsettings.json`を以下のように変更する。
"ConnectionStrings"を追加して接続文字列を記載する。

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=mydb;Username=postgres;Password=password"
  },
  "AllowedHosts": "*"
}
```

2. ApplicationDbContextクラスを作成する
EntityFrameworkからDBにアクセスする場合、DBContextクラス経由でDBのテーブルを操作する。
まずは、DbContextを継承した独自のクラスを作成する。今回は、`ApplicationDbContext.cs`とする。

```
using Microsoft.EntityFrameworkCore;

namespace BrigdeNgWebAPI.Models {
    public class ApplicationDbContext:DbContext {

        public ApplicationDbContext() : base() { }

        public ApplicationDbContext(DbContextOptions options) : base(options) {
        }
    }
}
```

3. `Program.cs`にDbContextの接続先を設定する
今回はPostgreSQLに接続するため、以下のように`Program.cs`を記載する。
これで、1.で設定した接続文字列を読み込んでDBに接続を行う。

```
using BrigdeNgWebAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//PostgreSQLを使用 <--追加
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

```

## コードファースト
### データモデルからテーブル生成
EntityFrameworkでは、データモデルを参照してテーブルを自動生成する。
例として市を表す`City.cs`データモデルを作成する。

プロパティの上部にアノテーションを付与することで、PKや、Not Null設定などを行うことができる。
[参考ページ](https://learn.microsoft.com/ja-jp/ef/core/modeling/entity-properties?tabs=data-annotations%2Cwithout-nrt)
```
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrigdeNgWebAPI.Data.Model {
    /// <summary>
    /// 市
    /// </summary>
    public class City {

        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 市名
        /// </summary>
        public string Name { get; set; } = null!;
    }
}
```

### 1:nの反映方法
例で、市を表すデータモデル`City.cs`を作成した。
1. 複数の市に対して1つの県が紐ずくような構造をデータモデルで表す。
まずは、県を表すデータモデル`Province.cs`を作成する。

```
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BrigdeNgWebAPI.Data.Model {
    /// <summary>
    /// 県
    /// </summary>
    public class Province {

        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 県名
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// 市のリスト
        /// </summary>
        public ICollection<City>? Cities { get; set; } = null!;
    }
}
```

2. `City.cs`を以下のように修正する
ProvinceIdとProvinceをプロパティに追加した。
アノテーションのForeignKeyをProvinceIdに付与することで、外部キーの設定を行う。
```
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrigdeNgWebAPI.Data.Model {
    /// <summary>
    /// 市
    /// </summary>
    public class City {

        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 市名
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// 県ID
        /// </summary>
        [ForeignKey(nameof(Province))]
        public int ProvinceId { get; set; }

        /// <summary>
        /// 県
        /// </summary>
        public Province? Province { get; set; } = null!;
    }
}
```

### 1:1の反映方法
データモデル`Shop.cs`に対してデータモデル`ShopType.cs`が1:1で紐ずくような構造をデータモデルで表す。
1. データモデル`Shop.cs`を作成する
```
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrigdeNgWebAPI.Data.Model {
    /// <summary>
    /// 店舗
    /// </summary>
    public class Shop {

        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 店舗名
        /// </summary>
        [Required]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 店舗の種類ID
        /// </summary>
        [ForeignKey(nameof(ShopType))]
        public int ShopTypeId { get; set; }

        /// <summary>
        /// 店舗の種類
        /// </summary>
        [Required]
        public ShopType? ShopType { get; set; } = null!;
    }
}
```

2. データモデル`ShopType.cs`を作成する
```
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BrigdeNgWebAPI.Data.Model {
    /// <summary>
    /// 店のタイプ
    /// </summary>
    [Index(nameof(Name))]
    public class ShopType {
        
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// タイプ名
        /// </summary>
        [Required]
        public string Name { get; set; } = null!;
    }
}
```

### データモデルをもとにDBのテーブルを生成する
1. データモデルをDbContextに追加する
`ApplicationDbcontext.cs`を以下のように修正して、データモデル情報を反映させる。

```
using Microsoft.EntityFrameworkCore;
using UdonMaestro_BackEnd.Data.Model;

namespace UdonMaestro_BackEnd.Data {
    public class ApplicationDbContext : DbContext {

        public ApplicationDbContext() : base() { }

        public ApplicationDbContext(DbContextOptions options) : base(options) {
        }

        //以下追加
        public DbSet<ShopType> ShopTypes => Set<ShopType>();        
        public DbSet<Province> Provinces => Set<Province>();
        public DbSet<City> Cities => Set<City>();        
        public DbSet<Shop> Shops => Set<Shop>();
    }
}

```

2. テーブル情報のマイグレーション実行
以下のコマンドをパッケージマネージャーコンソールで実行します。
`Add-Migration Initial -OutputDir "Data/Migrations"`
すると/Data/Migrations/以下にテーブルを生成するプログラムが自動生成されます。

3. マイグレーションをDBへ反映
2.でマイグレーションを行い、プログラムを自動生成しましたが、この段階ではDBに反映されていません。
DBに反映を行うには、以下のコマンドをパッケージマネージャコンソールで実行します。
`Update-Database`

### データモデルの変更をDBのテーブルに反映させる
データモデル`Province.cs`に人口(Population)の項目を追加したとします。
```
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BrigdeNgWebAPI.Data.Model {
    /// <summary>
    /// 県
    /// </summary>
    public class Province {

        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 県名
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// 市のリスト
        /// </summary>
        public ICollection<City>? Cities { get; set; } = null!;

        /// <summary>
        /// 人口 (追加)
        /// </summary>
        public int Population { get; set; }
    }
}
```
EntityFrameworkは、データモデルとDBのテーブル構造が食い違うと例外が発生するので、マイグレーションさせる必要がある。
以下のコマンドを実行してマイグレーションする。すると、テーブルにもPopulationの項目が追加される。
`Add-Migration AddPopulation -OutputDir "Data/Migrations"`
`Update-Database`

## コントローラ生成
### 取得系API

### 登録系API

### ページネーション機能を実装

## [DBの接続文字列をソース管理したくない場合](https://learn.microsoft.com/ja-jp/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows)
APIキーや、DBのパスワードなどソース管理したくない機密情報は、Secrets Storageを利用して秘匿することができます。

1. ソリューションを右クリックして、ユーザシークレットの管理を押します。
2. `secrets.json`が自動生成され、`\Users\UserName\AppData\Roaming\Microsot\UserSecrets`に保存されます。
3. 