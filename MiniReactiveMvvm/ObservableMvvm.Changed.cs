using System;
using System.Linq.Expressions;
using System.Reactive.Linq;

namespace MiniReactiveMvvm
{
    public static class ObservableMvvmChanged
    {
        public static IObservable<TResult> Changed<TObj, TProperty1, TProperty2, TResult>(
            this TObj viewModel,
            Expression<Func<TObj, TProperty1>> propertyExpr1,
            Expression<Func<TObj, TProperty2>> propertyExpr2,
            Func<TProperty1, TProperty2, TResult> combine)
        {
            return Observable.CombineLatest(
                viewModel.Changed(propertyExpr1),
                viewModel.Changed(propertyExpr2),
                combine);
        }
        public static IObservable<TResult> Changed<TObj, TProperty1, TProperty2, TProperty3, TResult>(
            this TObj viewModel,
            Expression<Func<TObj, TProperty1>> propertyExpr1,
            Expression<Func<TObj, TProperty2>> propertyExpr2,
            Expression<Func<TObj, TProperty3>> propertyExpr3,
            Func<TProperty1, TProperty2, TProperty3, TResult> combine)
        {
            return Observable.CombineLatest(
                viewModel.Changed(propertyExpr1),
                viewModel.Changed(propertyExpr2),
                viewModel.Changed(propertyExpr3),
                combine);
        }
        public static IObservable<TResult> Changed<TObj, TProperty1, TProperty2, TProperty3, TProperty4, TResult>(
            this TObj viewModel,
            Expression<Func<TObj, TProperty1>> propertyExpr1,
            Expression<Func<TObj, TProperty2>> propertyExpr2,
            Expression<Func<TObj, TProperty3>> propertyExpr3,
            Expression<Func<TObj, TProperty4>> propertyExpr4,
            Func<TProperty1, TProperty2, TProperty3, TProperty4, TResult> combine)
        {
            return Observable.CombineLatest(
                viewModel.Changed(propertyExpr1),
                viewModel.Changed(propertyExpr2),
                viewModel.Changed(propertyExpr3),
                viewModel.Changed(propertyExpr4),
                combine);
        }
        public static IObservable<TResult> Changed<TObj, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5, TResult>(
            this TObj viewModel,
            Expression<Func<TObj, TProperty1>> propertyExpr1,
            Expression<Func<TObj, TProperty2>> propertyExpr2,
            Expression<Func<TObj, TProperty3>> propertyExpr3,
            Expression<Func<TObj, TProperty4>> propertyExpr4,
            Expression<Func<TObj, TProperty5>> propertyExpr5,
            Func<TProperty1, TProperty2, TProperty3, TProperty4, TProperty5, TResult> combine)
        {
            return Observable.CombineLatest(
                viewModel.Changed(propertyExpr1),
                viewModel.Changed(propertyExpr2),
                viewModel.Changed(propertyExpr3),
                viewModel.Changed(propertyExpr4),
                viewModel.Changed(propertyExpr5),
                combine);
        }
        public static IObservable<TResult> Changed<TObj, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5, TProperty6, TResult>(
            this TObj viewModel,
            Expression<Func<TObj, TProperty1>> propertyExpr1,
            Expression<Func<TObj, TProperty2>> propertyExpr2,
            Expression<Func<TObj, TProperty3>> propertyExpr3,
            Expression<Func<TObj, TProperty4>> propertyExpr4,
            Expression<Func<TObj, TProperty5>> propertyExpr5,
            Expression<Func<TObj, TProperty6>> propertyExpr6,
            Func<TProperty1, TProperty2, TProperty3, TProperty4, TProperty5, TProperty6, TResult> combine)
        {
            return Observable.CombineLatest(
                viewModel.Changed(propertyExpr1),
                viewModel.Changed(propertyExpr2),
                viewModel.Changed(propertyExpr3),
                viewModel.Changed(propertyExpr4),
                viewModel.Changed(propertyExpr5),
                viewModel.Changed(propertyExpr6),
                combine);
        }
        public static IObservable<TResult> Changed<TObj, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5, TProperty6, TProperty7, TResult>(
            this TObj viewModel,
            Expression<Func<TObj, TProperty1>> propertyExpr1,
            Expression<Func<TObj, TProperty2>> propertyExpr2,
            Expression<Func<TObj, TProperty3>> propertyExpr3,
            Expression<Func<TObj, TProperty4>> propertyExpr4,
            Expression<Func<TObj, TProperty5>> propertyExpr5,
            Expression<Func<TObj, TProperty6>> propertyExpr6,
            Expression<Func<TObj, TProperty7>> propertyExpr7,
            Func<TProperty1, TProperty2, TProperty3, TProperty4, TProperty5, TProperty6, TProperty7, TResult> combine)
        {
            return Observable.CombineLatest(
                viewModel.Changed(propertyExpr1),
                viewModel.Changed(propertyExpr2),
                viewModel.Changed(propertyExpr3),
                viewModel.Changed(propertyExpr4),
                viewModel.Changed(propertyExpr5),
                viewModel.Changed(propertyExpr6),
                viewModel.Changed(propertyExpr7),
                combine);
        }
        public static IObservable<TResult> Changed<TObj, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5, TProperty6, TProperty7, TProperty8, TResult>(
            this TObj viewModel,
            Expression<Func<TObj, TProperty1>> propertyExpr1,
            Expression<Func<TObj, TProperty2>> propertyExpr2,
            Expression<Func<TObj, TProperty3>> propertyExpr3,
            Expression<Func<TObj, TProperty4>> propertyExpr4,
            Expression<Func<TObj, TProperty5>> propertyExpr5,
            Expression<Func<TObj, TProperty6>> propertyExpr6,
            Expression<Func<TObj, TProperty7>> propertyExpr7,
            Expression<Func<TObj, TProperty8>> propertyExpr8,
            Func<TProperty1, TProperty2, TProperty3, TProperty4, TProperty5, TProperty6, TProperty7, TProperty8, TResult> combine)
        {
            return Observable.CombineLatest(
                viewModel.Changed(propertyExpr1),
                viewModel.Changed(propertyExpr2),
                viewModel.Changed(propertyExpr3),
                viewModel.Changed(propertyExpr4),
                viewModel.Changed(propertyExpr5),
                viewModel.Changed(propertyExpr6),
                viewModel.Changed(propertyExpr7),
                viewModel.Changed(propertyExpr8),
                combine);
        }
        public static IObservable<TResult> Changed<TObj, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5, TProperty6, TProperty7, TProperty8, TProperty9, TResult>(
            this TObj viewModel,
            Expression<Func<TObj, TProperty1>> propertyExpr1,
            Expression<Func<TObj, TProperty2>> propertyExpr2,
            Expression<Func<TObj, TProperty3>> propertyExpr3,
            Expression<Func<TObj, TProperty4>> propertyExpr4,
            Expression<Func<TObj, TProperty5>> propertyExpr5,
            Expression<Func<TObj, TProperty6>> propertyExpr6,
            Expression<Func<TObj, TProperty7>> propertyExpr7,
            Expression<Func<TObj, TProperty8>> propertyExpr8,
            Expression<Func<TObj, TProperty9>> propertyExpr9,
            Func<TProperty1, TProperty2, TProperty3, TProperty4, TProperty5, TProperty6, TProperty7, TProperty8, TProperty9, TResult> combine)
        {
            return Observable.CombineLatest(
                viewModel.Changed(propertyExpr1),
                viewModel.Changed(propertyExpr2),
                viewModel.Changed(propertyExpr3),
                viewModel.Changed(propertyExpr4),
                viewModel.Changed(propertyExpr5),
                viewModel.Changed(propertyExpr6),
                viewModel.Changed(propertyExpr7),
                viewModel.Changed(propertyExpr8),
                viewModel.Changed(propertyExpr9),
                combine);
        }
        public static IObservable<TResult> Changed<TObj, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5, TProperty6, TProperty7, TProperty8, TProperty9, TProperty10, TResult>(
            this TObj viewModel,
            Expression<Func<TObj, TProperty1>> propertyExpr1,
            Expression<Func<TObj, TProperty2>> propertyExpr2,
            Expression<Func<TObj, TProperty3>> propertyExpr3,
            Expression<Func<TObj, TProperty4>> propertyExpr4,
            Expression<Func<TObj, TProperty5>> propertyExpr5,
            Expression<Func<TObj, TProperty6>> propertyExpr6,
            Expression<Func<TObj, TProperty7>> propertyExpr7,
            Expression<Func<TObj, TProperty8>> propertyExpr8,
            Expression<Func<TObj, TProperty9>> propertyExpr9,
            Expression<Func<TObj, TProperty10>> propertyExpr10,
            Func<TProperty1, TProperty2, TProperty3, TProperty4, TProperty5, TProperty6, TProperty7, TProperty8, TProperty9, TProperty10, TResult> combine)
        {
            return Observable.CombineLatest(
                viewModel.Changed(propertyExpr1),
                viewModel.Changed(propertyExpr2),
                viewModel.Changed(propertyExpr3),
                viewModel.Changed(propertyExpr4),
                viewModel.Changed(propertyExpr5),
                viewModel.Changed(propertyExpr6),
                viewModel.Changed(propertyExpr7),
                viewModel.Changed(propertyExpr8),
                viewModel.Changed(propertyExpr9),
                viewModel.Changed(propertyExpr10),
                combine);
        }
        public static IObservable<TResult> Changed<TObj, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5, TProperty6, TProperty7, TProperty8, TProperty9, TProperty10, TProperty11, TResult>(
            this TObj viewModel,
            Expression<Func<TObj, TProperty1>> propertyExpr1,
            Expression<Func<TObj, TProperty2>> propertyExpr2,
            Expression<Func<TObj, TProperty3>> propertyExpr3,
            Expression<Func<TObj, TProperty4>> propertyExpr4,
            Expression<Func<TObj, TProperty5>> propertyExpr5,
            Expression<Func<TObj, TProperty6>> propertyExpr6,
            Expression<Func<TObj, TProperty7>> propertyExpr7,
            Expression<Func<TObj, TProperty8>> propertyExpr8,
            Expression<Func<TObj, TProperty9>> propertyExpr9,
            Expression<Func<TObj, TProperty10>> propertyExpr10,
            Expression<Func<TObj, TProperty11>> propertyExpr11,
            Func<TProperty1, TProperty2, TProperty3, TProperty4, TProperty5, TProperty6, TProperty7, TProperty8, TProperty9, TProperty10, TProperty11, TResult> combine)
        {
            return Observable.CombineLatest(
                viewModel.Changed(propertyExpr1),
                viewModel.Changed(propertyExpr2),
                viewModel.Changed(propertyExpr3),
                viewModel.Changed(propertyExpr4),
                viewModel.Changed(propertyExpr5),
                viewModel.Changed(propertyExpr6),
                viewModel.Changed(propertyExpr7),
                viewModel.Changed(propertyExpr8),
                viewModel.Changed(propertyExpr9),
                viewModel.Changed(propertyExpr10),
                viewModel.Changed(propertyExpr11),
                combine);
        }
        public static IObservable<TResult> Changed<TObj, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5, TProperty6, TProperty7, TProperty8, TProperty9, TProperty10, TProperty11, TProperty12, TResult>(
            this TObj viewModel,
            Expression<Func<TObj, TProperty1>> propertyExpr1,
            Expression<Func<TObj, TProperty2>> propertyExpr2,
            Expression<Func<TObj, TProperty3>> propertyExpr3,
            Expression<Func<TObj, TProperty4>> propertyExpr4,
            Expression<Func<TObj, TProperty5>> propertyExpr5,
            Expression<Func<TObj, TProperty6>> propertyExpr6,
            Expression<Func<TObj, TProperty7>> propertyExpr7,
            Expression<Func<TObj, TProperty8>> propertyExpr8,
            Expression<Func<TObj, TProperty9>> propertyExpr9,
            Expression<Func<TObj, TProperty10>> propertyExpr10,
            Expression<Func<TObj, TProperty11>> propertyExpr11,
            Expression<Func<TObj, TProperty12>> propertyExpr12,
            Func<TProperty1, TProperty2, TProperty3, TProperty4, TProperty5, TProperty6, TProperty7, TProperty8, TProperty9, TProperty10, TProperty11, TProperty12, TResult> combine)
        {
            return Observable.CombineLatest(
                viewModel.Changed(propertyExpr1),
                viewModel.Changed(propertyExpr2),
                viewModel.Changed(propertyExpr3),
                viewModel.Changed(propertyExpr4),
                viewModel.Changed(propertyExpr5),
                viewModel.Changed(propertyExpr6),
                viewModel.Changed(propertyExpr7),
                viewModel.Changed(propertyExpr8),
                viewModel.Changed(propertyExpr9),
                viewModel.Changed(propertyExpr10),
                viewModel.Changed(propertyExpr11),
                viewModel.Changed(propertyExpr12),
                combine);
        }
        public static IObservable<TResult> Changed<TObj, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5, TProperty6, TProperty7, TProperty8, TProperty9, TProperty10, TProperty11, TProperty12, TProperty13, TResult>(
            this TObj viewModel,
            Expression<Func<TObj, TProperty1>> propertyExpr1,
            Expression<Func<TObj, TProperty2>> propertyExpr2,
            Expression<Func<TObj, TProperty3>> propertyExpr3,
            Expression<Func<TObj, TProperty4>> propertyExpr4,
            Expression<Func<TObj, TProperty5>> propertyExpr5,
            Expression<Func<TObj, TProperty6>> propertyExpr6,
            Expression<Func<TObj, TProperty7>> propertyExpr7,
            Expression<Func<TObj, TProperty8>> propertyExpr8,
            Expression<Func<TObj, TProperty9>> propertyExpr9,
            Expression<Func<TObj, TProperty10>> propertyExpr10,
            Expression<Func<TObj, TProperty11>> propertyExpr11,
            Expression<Func<TObj, TProperty12>> propertyExpr12,
            Expression<Func<TObj, TProperty13>> propertyExpr13,
            Func<TProperty1, TProperty2, TProperty3, TProperty4, TProperty5, TProperty6, TProperty7, TProperty8, TProperty9, TProperty10, TProperty11, TProperty12, TProperty13, TResult> combine)
        {
            return Observable.CombineLatest(
                viewModel.Changed(propertyExpr1),
                viewModel.Changed(propertyExpr2),
                viewModel.Changed(propertyExpr3),
                viewModel.Changed(propertyExpr4),
                viewModel.Changed(propertyExpr5),
                viewModel.Changed(propertyExpr6),
                viewModel.Changed(propertyExpr7),
                viewModel.Changed(propertyExpr8),
                viewModel.Changed(propertyExpr9),
                viewModel.Changed(propertyExpr10),
                viewModel.Changed(propertyExpr11),
                viewModel.Changed(propertyExpr12),
                viewModel.Changed(propertyExpr13),
                combine);
        }
        public static IObservable<TResult> Changed<TObj, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5, TProperty6, TProperty7, TProperty8, TProperty9, TProperty10, TProperty11, TProperty12, TProperty13, TProperty14, TResult>(
            this TObj viewModel,
            Expression<Func<TObj, TProperty1>> propertyExpr1,
            Expression<Func<TObj, TProperty2>> propertyExpr2,
            Expression<Func<TObj, TProperty3>> propertyExpr3,
            Expression<Func<TObj, TProperty4>> propertyExpr4,
            Expression<Func<TObj, TProperty5>> propertyExpr5,
            Expression<Func<TObj, TProperty6>> propertyExpr6,
            Expression<Func<TObj, TProperty7>> propertyExpr7,
            Expression<Func<TObj, TProperty8>> propertyExpr8,
            Expression<Func<TObj, TProperty9>> propertyExpr9,
            Expression<Func<TObj, TProperty10>> propertyExpr10,
            Expression<Func<TObj, TProperty11>> propertyExpr11,
            Expression<Func<TObj, TProperty12>> propertyExpr12,
            Expression<Func<TObj, TProperty13>> propertyExpr13,
            Expression<Func<TObj, TProperty14>> propertyExpr14,
            Func<TProperty1, TProperty2, TProperty3, TProperty4, TProperty5, TProperty6, TProperty7, TProperty8, TProperty9, TProperty10, TProperty11, TProperty12, TProperty13, TProperty14, TResult> combine)
        {
            return Observable.CombineLatest(
                viewModel.Changed(propertyExpr1),
                viewModel.Changed(propertyExpr2),
                viewModel.Changed(propertyExpr3),
                viewModel.Changed(propertyExpr4),
                viewModel.Changed(propertyExpr5),
                viewModel.Changed(propertyExpr6),
                viewModel.Changed(propertyExpr7),
                viewModel.Changed(propertyExpr8),
                viewModel.Changed(propertyExpr9),
                viewModel.Changed(propertyExpr10),
                viewModel.Changed(propertyExpr11),
                viewModel.Changed(propertyExpr12),
                viewModel.Changed(propertyExpr13),
                viewModel.Changed(propertyExpr14),
                combine);
        }
        public static IObservable<TResult> Changed<TObj, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5, TProperty6, TProperty7, TProperty8, TProperty9, TProperty10, TProperty11, TProperty12, TProperty13, TProperty14, TProperty15, TResult>(
            this TObj viewModel,
            Expression<Func<TObj, TProperty1>> propertyExpr1,
            Expression<Func<TObj, TProperty2>> propertyExpr2,
            Expression<Func<TObj, TProperty3>> propertyExpr3,
            Expression<Func<TObj, TProperty4>> propertyExpr4,
            Expression<Func<TObj, TProperty5>> propertyExpr5,
            Expression<Func<TObj, TProperty6>> propertyExpr6,
            Expression<Func<TObj, TProperty7>> propertyExpr7,
            Expression<Func<TObj, TProperty8>> propertyExpr8,
            Expression<Func<TObj, TProperty9>> propertyExpr9,
            Expression<Func<TObj, TProperty10>> propertyExpr10,
            Expression<Func<TObj, TProperty11>> propertyExpr11,
            Expression<Func<TObj, TProperty12>> propertyExpr12,
            Expression<Func<TObj, TProperty13>> propertyExpr13,
            Expression<Func<TObj, TProperty14>> propertyExpr14,
            Expression<Func<TObj, TProperty15>> propertyExpr15,
            Func<TProperty1, TProperty2, TProperty3, TProperty4, TProperty5, TProperty6, TProperty7, TProperty8, TProperty9, TProperty10, TProperty11, TProperty12, TProperty13, TProperty14, TProperty15, TResult> combine)
        {
            return Observable.CombineLatest(
                viewModel.Changed(propertyExpr1),
                viewModel.Changed(propertyExpr2),
                viewModel.Changed(propertyExpr3),
                viewModel.Changed(propertyExpr4),
                viewModel.Changed(propertyExpr5),
                viewModel.Changed(propertyExpr6),
                viewModel.Changed(propertyExpr7),
                viewModel.Changed(propertyExpr8),
                viewModel.Changed(propertyExpr9),
                viewModel.Changed(propertyExpr10),
                viewModel.Changed(propertyExpr11),
                viewModel.Changed(propertyExpr12),
                viewModel.Changed(propertyExpr13),
                viewModel.Changed(propertyExpr14),
                viewModel.Changed(propertyExpr15),
                combine);
        }
        public static IObservable<TResult> Changed<TObj, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5, TProperty6, TProperty7, TProperty8, TProperty9, TProperty10, TProperty11, TProperty12, TProperty13, TProperty14, TProperty15, TProperty16, TResult>(
            this TObj viewModel,
            Expression<Func<TObj, TProperty1>> propertyExpr1,
            Expression<Func<TObj, TProperty2>> propertyExpr2,
            Expression<Func<TObj, TProperty3>> propertyExpr3,
            Expression<Func<TObj, TProperty4>> propertyExpr4,
            Expression<Func<TObj, TProperty5>> propertyExpr5,
            Expression<Func<TObj, TProperty6>> propertyExpr6,
            Expression<Func<TObj, TProperty7>> propertyExpr7,
            Expression<Func<TObj, TProperty8>> propertyExpr8,
            Expression<Func<TObj, TProperty9>> propertyExpr9,
            Expression<Func<TObj, TProperty10>> propertyExpr10,
            Expression<Func<TObj, TProperty11>> propertyExpr11,
            Expression<Func<TObj, TProperty12>> propertyExpr12,
            Expression<Func<TObj, TProperty13>> propertyExpr13,
            Expression<Func<TObj, TProperty14>> propertyExpr14,
            Expression<Func<TObj, TProperty15>> propertyExpr15,
            Expression<Func<TObj, TProperty16>> propertyExpr16,
            Func<TProperty1, TProperty2, TProperty3, TProperty4, TProperty5, TProperty6, TProperty7, TProperty8, TProperty9, TProperty10, TProperty11, TProperty12, TProperty13, TProperty14, TProperty15, TProperty16, TResult> combine)
        {
            return Observable.CombineLatest(
                viewModel.Changed(propertyExpr1),
                viewModel.Changed(propertyExpr2),
                viewModel.Changed(propertyExpr3),
                viewModel.Changed(propertyExpr4),
                viewModel.Changed(propertyExpr5),
                viewModel.Changed(propertyExpr6),
                viewModel.Changed(propertyExpr7),
                viewModel.Changed(propertyExpr8),
                viewModel.Changed(propertyExpr9),
                viewModel.Changed(propertyExpr10),
                viewModel.Changed(propertyExpr11),
                viewModel.Changed(propertyExpr12),
                viewModel.Changed(propertyExpr13),
                viewModel.Changed(propertyExpr14),
                viewModel.Changed(propertyExpr15),
                viewModel.Changed(propertyExpr16),
                combine);
        }
    }
}